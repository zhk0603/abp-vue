#!/bin/sh

cd /abpvue/vue/
ls -l

docker build -t abpvue_frontend:v$1 .
docker tag abpvue_frontend:v$1 abpvue_frontend:latest

# 停止
if [ "$(docker ps -q -f name=abpvue_frontend)" ]; then
    docker stop abpvue_frontend
fi

# 删除
if [ "$(docker ps -qa -f name=abpvue_frontend)" ]; then
    docker rm abpvue_frontend
fi

#启动新容器
docker run --name abpvue_frontend -d -p 8080:80 --link abpvue_idp-api_1:idp-api --link abpvue_host-api_1:host-api --network abpvue_default abpvue_frontend:latest