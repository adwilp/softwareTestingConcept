### Build ###
FROM node:18-alpine3.15 AS build
WORKDIR /usr/src/app
COPY package.json package-lock.json ./
RUN npm install
COPY . .

FROM build as test-env
RUN apk add chromium
ENV CHROME_BIN=/usr/bin/chromium-browser

FROM test-env as test
RUN npm run lint:ci
RUN npm run test:ci

FROM build as publish
ARG ENV
RUN npm run build-$ENV

FROM scratch AS export-test
COPY --from=test /usr/src/app/testresults/junit/unit-test-result.xml /testresults/unit-test-result.xml
COPY --from=test /usr/src/app/testresults/coverage/code-coverage.xml /testresults/code-coverage.xml

FROM scratch AS export-app
ARG ENV
COPY --from=publish /usr/src/app/dist/$ENV /app
