#!/bin/sh

echo "[wait-for-rabbitmq] Waiting for RabbitMQ at $RABBITMQ_HOST:$RABBITMQ_PORT..."

while ! nc -z $RABBITMQ_HOST $RABBITMQ_PORT; do
  echo "[wait-for-rabbitmq] RabbitMQ not available yet. Retrying in 2s..."
  sleep 2
done

echo "[wait-for-rabbitmq] RabbitMQ is up! Starting application..."
exec "$@"
