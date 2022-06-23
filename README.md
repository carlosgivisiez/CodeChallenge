# CodeChallenge
## Tools needed

Everything you will need in order to run the projects. Get everything ready before following the next steps.
* [Visual Studio]([https://](https://visualstudio.microsoft.com/downloads/))
* [Docker](https://www.docker.com/products/docker-desktop/)
* [Node](https://nodejs.org/en/download/)
* [Yarn](https://classic.yarnpkg.com/lang/en/docs/install/#windows-stable)

## Preparing
### Redis

Open you favorite command line tool and follow the step-by-step below
1. `docker pull redis/redis-stack`
2. `docker run -d --name redis -p 6379:6379 redis/redis-stack:latest`

### React

1. Open you favorite command line tool in the directory **src/code-challenge** relative to the repository root
2. Run `yarn install`

### Visual Studio solution

1. Open The **CodeChallenge.sln**
2. Set the following startup projects
   * CodeChallenge.Auth.Admin
   * CodeChallenge.Auth.Admin.Api
   * CodeChallenge.Auth.STS.Identity
   * CodeChallenge.Chatroom.Api

## Running
### Frontend

1. Open you favorite command line tool in the directory **src/code-challenge** relative to the repository root
2. Run `yarn start`

### Backend

1. Open The **CodeChallenge.sln**
2. Start the solution

## Considerations
### Enhancements

* Use Docker Compose to setup a local development environmet
* Make use of all the potential provided by [Skoruba.Duende.IdentityServer.Admin]([https://](https://github.com/skoruba/Duende.IdentityServer.Admin))
* Implement chat bots, e.g **Stock bot**, as a client of the IdentityServer. This way we can provide a scalable way to have different chat bots written in different languages running in different servers.

### Problems
* The Redis Repositoy implementation has poor performance. It needs a little more time in order to implement it properly.