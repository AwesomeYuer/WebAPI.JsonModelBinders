# WebAPI JsonModelBinders

## JTokenModelBinder
```
Newtonsoft.Json.Linq.JToken
```

## JsonNodeModelBinder
```
System.Text.Json.Nodes.JsonNode
```

## 以下说明由 `ChatGPT` 生成

Web API JsonModelBinders是为了帮助开发人员轻松在API控制器中传递JSON作为参数或结果而创建的。可用的JsonModelBinder类型有JTokenModelBinders、Newtonsoft.Json.Linq.JToken和System.Text.Json.Nodes.JsonNode，它们可以帮助开发人员使用JsonNode、JToken或JObject作为参数。开发人员可以在API控制器中定义具有带有ModelBinder特性（接受JsonNode类型的参数）的EchoJsonNode方法，然后返回JsonNode参数以显示结果。要测试此技术，可以使用.http文件，例如WebAPI.JTokenModelBinder / VSCode.Rest.Client.Test / RestClientTest.http。

## The following instructions were generated by `ChatGPT`

Web API JsonModelBinders provide an easy way for developers to pass JSON as parameters or results. Available JsonModelBinder types are JTokenModelBinders, Newtonsoft.Json.Linq.JToken, and System.Text.Json.Nodes.JsonNode, which help developers use JsonNode, JToken, and JObject as parameters in API controllers. An example usage is defining an API controller with an EchoJson node method that has a ModelBinder attribute accepting a jsonNode parameter and then returning it to display results. This technology can be tested by using a .http file, such as WebAPI.JTokenModelBinder/VSCode.Rest.Client.Test/RestClientTest.http.

* Support All http methods pass JsonNode/JToken parameter

    ```
        [HttpDelete]
        [HttpGet]
        [HttpHead]
        [HttpOptions]
        [HttpPatch]
        [HttpPost]
        [HttpPut]
    ```
* Support json and url encode query string and form body
    ```http
    ###
    GET  https://localhost:7095/api/admin/echo/jsonnode/asdasd?a=111&b=2&a=a222 HTTP/1.1

    ###
    GET  https://localhost:7095/api/admin/echo/jsonnode/async/asdasd?a=111&b=2&a=a222 HTTP/1.1


    ###
    # ok
    POST  https://localhost:7095/api/admin/echo/jsonnode/a/bbbbb?a=111&b=2 HTTP/1.1
    content-type: application/x-www-form-urlencoded

    sql=set+statistics+io+on%0Aset+statistics+time+on%0Aset+statistics+profile+on%0Aselect+'%22111%22'+as+F%2C+*%0Afrom%0Asys.objects%0A%0Aselect+'%22222%22'+as+F%2C+*%0Afrom%0Asys.objects&rowcount=100


    ###
    # ok
    POST  https://localhost:7095/api/admin/echo/jsonnode/async/bbbbb?a=111&b=2 HTTP/1.1
    content-type: application/x-www-form-urlencoded

    sql=set+statistics+io+on%0Aset+statistics+time+on%0Aset+statistics+profile+on%0Aselect+'%22111%22'+as+F%2C+*%0Afrom%0Asys.objects%0A%0Aselect+'%22222%22'+as+F%2C+*%0Afrom%0Asys.objects&rowcount=100


    ###
    # ok
    POST  https://localhost:7095/api/admin/echo/jsonnode/async/bbbbb HTTP/1.1
    content-type: application/json

    { "a": 1}

    ###
    # failed
    POST  https://localhost:7095/api/admin/echo/jsonnode/a/bbbbb?a=111&b=2 HTTP/1.1
    content-type: application/json


    ###
    #已修复四选一有Bug
    # AmbiguousMatchException
    POST  https://localhost:7095/api/admin/echo/jsonnode HTTP/1.1
    content-type: application/json

    {"a":9999,"b":[1,2,3,5]}

    ###
    #已修复四选一有Bug
    # AmbiguousMatchException
    # Async 大写 A 则 失败
    POST  https://localhost:7095/api/admin/echo/jsonnode/async/aaa?a=10 HTTP/1.1
    content-type: application/json

    {"a":9999,"b":[1,2,3,5] }

    ###
    PUT  https://localhost:7095/api/admin/echo/jsonnode?a=111&b=2 HTTP/1.1
    content-type: application/json

    {}

    ###
    PUT  https://localhost:7095/api/admin/echo/jsonnode/async/aa/zz HTTP/1.1
    content-type: application/json

    {"a":1}

    ###

    GET  https://localhost:7095/api/admin/echo/jsonnode/async/sadasd/asdasd?{a:19} HTTP/1.1


    ```

## Usage Sample code:

```csharp
namespace WebApplication1.Controllers;

using Microshaoft;
using Microshaoft.Web;
using Microshaoft.WebApi.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

[ConstrainedRoute("api/[controller]")]
[ApiController]
[Route("api/[controller]")]
public partial class AdminController : ControllerBase
{
    //private readonly ILogger<AdminController> _logger;

    //public AdminController(ILogger<AdminController> logger)
    //{
    //    _logger = logger;
    //}

    [HttpDelete]
    [HttpGet]
    [HttpHead]
    [HttpOptions]
    [HttpPatch]
    [HttpPost]
    [HttpPut]
    [Route("Echo/JsonNode/{* }")]
    public ActionResult EchoJsonNode
            (
                 [ModelBinder(typeof(JsonNodeModelBinder))]
                    JsonNode parameters //= null!
            )
    {
        var (callerMemberName, callerFilePath, callerLineNumber) = CallerHelper.GetCallerInfo();

        return
            Request
                .EchoJsonRequestJsonResult<JsonNode>
                    (
                        parameters
                        , new
                        {
                            Caller = new
                            {
                                callerMemberName
                                , callerFilePath
                                , callerLineNumber
                            }
                        }
                    );
    }
}
```

## For Testing use `.http` file as below: 
```
WebAPI.JTokenModelBinder\VSCode.Rest.Client.Test\RestClientTest.http
```




[![Build Status](https://microshaoft.visualstudio.com/sample-project-001/_apis/build/status/Microshaoft.WebAPI.JTokenModelBinder?branchName=master)](https://microshaoft.visualstudio.com/sample-project-001/_build/latest?definitionId=17&branchName=master)
[![Build Status](https://microshaoft.visualstudio.com/sample-project-001/_apis/build/status/Microshaoft.WebAPI.JTokenModelBinder?branchName=master)](https://microshaoft.visualstudio.com/sample-project-001/_build/latest?definitionId=17&branchName=master)