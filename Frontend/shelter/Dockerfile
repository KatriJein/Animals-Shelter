
FROM node:alpine AS development

ENV NODE_ENV development

WORKDIR /react-app
EXPOSE 3000
EXPOSE 3001

COPY ./package*.json /react-app

RUN npm install

COPY . .

CMD ["npm","start"]