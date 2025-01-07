
FROM node:alpine AS build

WORKDIR /react-app

COPY ./package*.json /react-app

ENV REACT_APP_API_URL="http://localhost/api"

RUN npm install
COPY . .
RUN npm run build

FROM nginx:stable-alpine
COPY --from=build /react-app/build /usr/share/nginx/html
COPY --from=build /react-app/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 3000
CMD ["nginx","-g", "daemon off;"]