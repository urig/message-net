# message-net
`message-net` is a tiny microservice for message based communication. 

### Build 

1. Clone 
2. `cd MessagesService`
3. Build with `docker build -t message-net:0.1 .`

## Run
Run with `docker run --rm -p "127.0.0.1:49164:80" message-net:0.1`.

Swagger UI is then available at [http://localhost:49164/swagger/](http://localhost:49164/swagger/).
