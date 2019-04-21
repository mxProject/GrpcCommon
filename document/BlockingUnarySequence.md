```mermaid
sequenceDiagram
participant cli as Client
participant inv as CallInvoker
participant svr as Server
participant svc as Service

cli ->> inv : Call Method&lt;TRes,TReq&gt;
inv ->> inv : Serialize request to byteArray
inv -->> svr : Invoke Method&lt;byte[],byte[]&gt;
svr ->> svr : Deserialize byteArray to request
svr ->> svc : Call MethodHandler&lt;TRes,TReq&gt;
svc ->> svr : Return response
svr ->> svr : Serialize response to byteArray
svr -->> inv : Return byteArray
inv ->> inv : Deserialize byteArray to response
inv ->> cli : Return response
```
