```mermaid
sequenceDiagram
participant cli as Client
participant cal as AsyncCall&lt;TRes,TReq&gt;
participant inv as CallInvoker
participant svr as Server
participant svc as Service

note right of cli : Invoke
cli ->> inv : Call Method&lt;TRes,TReq&gt;
inv -->> svr : Invoke Method&lt;byte[],byte[]&gt;
svr ->> svc : Call MethodHandler&lt;TRes,TReq&gt;
svr -->> inv : Return asyncCall&lt;byte[],byte[]&gt;
inv ->>+ cal : Wrap as asyncCall&lt;TRes,TReq&gt;
inv ->> cli : Return asyncCall&lt;TRes,TReq&gt;

note right of cli : Send request
cli ->> cal : Send request
cal ->> cal : Serialize request to byteArray
cal -->> svr : Send byteArray
svr ->> svr : Deserialize byteArray to request
svr ->> svc : Send request

note left of svc : Return response
svc ->> svr : Return response
svr ->> svr : Serialize response to byteArray
svr -->> cal : Receive byteArray
cal ->> cal : Deserialize byteArray to response
cal ->> cli : Receive response

cli ->>- cal : Dispose

```