FROM ruby:2.5

ARG PRODUCTION_DATABASE_PASSWORD
ENV PRODUCTION_DATABASE_PASSWORD=${PRODUCTION_DATABASE_PASSWORD}

ARG RAILS_MASTER_KEY
ENV RAILS_MASTER_KEY ${RAILS_MASTER_KEY}

RUN apt-get update -qq && apt-get install -y nodejs vim

WORKDIR /app
COPY ./src/Gemfile /app/Gemfile
COPY ./src/Gemfile.lock /app/Gemfile.lock
RUN bundle install

COPY ./src /app

COPY entrypoint.sh /usr/bin/
RUN chmod +x /usr/bin/entrypoint.sh
ENTRYPOINT ["entrypoint.sh"]
EXPOSE 3000

CMD ["rails", "server", "-b", "0.0.0.0"]