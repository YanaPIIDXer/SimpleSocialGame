variable image_uri {}
resource "aws_ecs_task_definition" "taskdef" {
    family = "SimpleSocialGame"
    network_mode = "bridge"
    cpu = 512
    memory = 512
    execution_role_arn = "arn:aws:iam::310815347645:role/ecsTaskExecutionRole"
    container_definitions = jsonencode(
        [
            {
                name = "server"
                image = var.image_uri
                portMappings = [
                    {
                        containerPort = 3000
                        hostPort = 0
                    }
                ]
                environment = [
                    { "name": "RAILS_ENV", "value": "production" },
                ]
            }
        ]
    )
}

resource "aws_ecs_cluster" "main" {
    name = "SimpleSocialGame"
}

data "template_file" "template" {
    template = <<EOF
#!/bin/bash
echo ECS_CLUSTER=SimpleSocialGame >> /etc/ecs/ecs.config
echo ECS_BACKEND_HOST= >> /etc/ecs/ecs.config
EOF
}

resource "aws_security_group" "ecs_instance_sg" {
    name = "SimpleSocialGame_ECSInstance"
    description = "ECS Instance"
    vpc_id = aws_vpc.main.id

    ingress {
        from_port        = 0
        to_port          = 0
        protocol         = "-1"
        security_groups  = [aws_security_group.lb_sg.id]
    }

    ingress {
        from_port        = 22
        to_port          = 22
        protocol         = "TCP"
        cidr_blocks      = ["0.0.0.0/0"]
    }

    egress {
        from_port        = 0
        to_port          = 0
        protocol         = "-1"
        cidr_blocks      = ["0.0.0.0/0"]
        ipv6_cidr_blocks = ["::/0"]
    }
}

resource "aws_launch_configuration" "instance_launch_conf" {
    name          = "SimpleSocialGame"
    image_id      = "ami-0d4cb7ae9a06c40c9"
    instance_type = "m5zn.large"
    iam_instance_profile = "ecsInstanceRole"
    security_groups = [aws_security_group.ecs_instance_sg.id]
    user_data = base64encode(data.template_file.template.rendered)
    associate_public_ip_address = true
    key_name = "key"
}

resource "aws_autoscaling_group" "autoscale_group" {
    vpc_zone_identifier = [aws_subnet.public1.id, aws_subnet.public2.id]
    desired_capacity   = 1
    max_size           = 1
    min_size           = 1
    launch_configuration = aws_launch_configuration.instance_launch_conf.name
    depends_on = [aws_launch_configuration.instance_launch_conf]
}

resource "aws_ecs_capacity_provider" "capacity" {
    name = "SimpleSocialGame"

    auto_scaling_group_provider {
        auto_scaling_group_arn         = aws_autoscaling_group.autoscale_group.arn

        managed_scaling {
            maximum_scaling_step_size = 1000
            minimum_scaling_step_size = 1
            status                    = "ENABLED"
            target_capacity           = 1
        }
    }
}

resource "aws_ecs_service" "service" {
    name            = "SimpleSocialGame"
    cluster         = aws_ecs_cluster.main.id
    task_definition = aws_ecs_task_definition.taskdef.arn
    desired_count   = 1

    load_balancer {
        target_group_arn = aws_lb_target_group.group.arn
        container_name   = "server"
        container_port   = 3000
    }
}
