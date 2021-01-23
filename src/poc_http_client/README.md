# HTTP CLIENT 

Ideia é juntar a implementacao de cache, resilencia(retry), e http client em uma lib. 

- cache
- payload (json, plain, form url encoded)
- retry
- timeout
- Serializer e Deserializer
## Definição
Classe Builder tem os metodos http "basicos" para um crud (GET, POST, PUT*, PATH*, DELETE), apos definido qual
metodo sera usado, sera agregado as outras informaçoes necessarias para a comunicação como url, headers, payload
sendo possivel definir nessa etapa se sera utilizado retry e se a resposta, caso seja get, sera salva no cache.
Finalizada a adição dos informações executando o metodo Send sera enviado a solicitacao , caso o metodo http seja 
Get o Send tem uma sobrecarga para utilizar o cache, informando chave, tempo de vida do cache e unidade do tempo. 
Apos isso sera retornado um Classe ResponseBase, tem informacoes como status code, headers, e conteudo.

## Configuracao 

```
    // IServiceCollection services
    services.AddTransient<IBuilder, Builder>();
    //ou    
    services.AddHttpClient<IBuilder, Builder>();
```
e configurar variavel de ambinete caso for usar o cache(redis) REDIS_BASE_URL

## Exemplos
GET
```
    var response = await _client
        .Get()
        .Url("http://localhost:3000/content")
        .Send();
        
    response.StatusCode(); // 2xx|3xx|4xx|5xx..
    response.Content(); // string
```

GET com cache
```
    var response = await _client
        .Get()
        .Url("http://localhost:3000/content")
        .Send( "mockcached", TTLUnit.Minutes, 1);
    
    response.StatusCode(); // 2xx|3xx|4xx|5xx..
    response.Content(); // string
```

GET com deserializer 
```
   class Mensagem {
        public string Conteudo {get; set;}
   }
    
    .
    .
    .

    var response = await _client
        .Get()
        .Url("http://localhost:3000/content")
        .Send();
    
    response.StatusCode(); // 2xx|3xx|4xx|5xx..
    Mensagem mensagem =  response.Content<Mensagem>(); // Mensagem
```


POST com serializer
```
   class Mensagem {
        public string Conteudo {get; set;}
   }
    
    .
    .
    .

   var message = new Mensagem {
        Conteudo = "hello_word"
    };
   var json = JsonContent.Create(message);

   var response = await _client
       .Post()
       .Url("http://localhost:3000/content")
       .AddJson<Mensagem>(message)
       .Send();
    
   response.StatusCode(); // 2xx|3xx|4xx|5xx..
   Mensagem mensagem =  response.Content<Mensagem>(); // Mensagem
```







* Para evitar polemicas tem os dois 