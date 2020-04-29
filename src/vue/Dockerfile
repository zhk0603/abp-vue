FROM nginx:alpine AS base
EXPOSE 80
EXPOSE 443

FROM node:lts-alpine as build
WORKDIR /src
COPY package*.json .
RUN npm install -g cnpm --registry=https://registry.npm.taobao.org && cnpm install
COPY . .
RUN npm run build:prod

FROM base AS final
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /src/dist /usr/share/nginx/html/