resource "aws_lb" "main" {
    name = "SimpleSocialGame"
    internal = false
    load_balancer_type = "application"
    subnets = [aws_subnet.public1.id, aws_subnet.public2.id]
    security_groups = [aws_security_group.lb_sg.id]
}

resource "aws_security_group" "lb_sg" {
    name = "SimpleSocialGame_LoadBalancer"
    description = "LoadBalancer"
    vpc_id = aws_vpc.main.id
    
    ingress {
        from_port        = 80
        to_port          = 80
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

resource "aws_lb_listener" "listener" {
  load_balancer_arn = aws_lb.main.arn
  port              = "80"
  protocol          = "HTTP"

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.group.arn
  }
}

resource "aws_lb_target_group" "group" {
    name     = "SimpleSocialGameTest"
    port     = 3000
    protocol = "HTTP"
    vpc_id   = aws_vpc.main.id
    depends_on = [aws_lb.main]
}

resource "aws_route53_record" "elb_domain_record" {
    zone_id = data.aws_route53_zone.rdb_domain_zone.zone_id
    name    = "simple-social-game.yanap-apptest.tk"
    type    = "CNAME"
    ttl     = "300"
    records = [aws_lb.main.dns_name]
}
