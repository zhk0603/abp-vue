FROM nginx:alpine 
EXPOSE 80
EXPOSE 443

COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY ./dist/ /usr/share/nginx/html/