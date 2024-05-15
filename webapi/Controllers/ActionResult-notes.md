# Examples of IActionResult implementations

Here are some of the types that implement IActionResult:

ContentResult: Represents a text result. A ContentResult object contains a Content property to hold the text and a ContentType property to hold the MIME type of the content.
```
return new ContentResult
{
    ContentType = "text/plain",
    Content = "Hello, World!",
    StatusCode = 200
};
```
JsonResult: Represents a JSON result that can be used to send JSON content to the response.
```
return new JsonResult(new { message = "Hello, World!" });
```

ViewResult: Represents an HTML result that is used to render a view.
```
return View();
```
PartialViewResult: Represents an HTML result that is used to render a partial view.
```
return PartialView("_PartialViewName");
```
FileResult: Represents a base class that is used to send binary file content to the response. There are several derived classes like FileContentResult, FileStreamResult, and VirtualFileResult.
```
byte[] fileBytes = System.IO.File.ReadAllBytes("filepath");
return new FileContentResult(fileBytes, "application/octet-stream")
{
    FileDownloadName = "filename.extension"
};
```
RedirectResult: Represents a redirection to a new URL.
```
return Redirect("http://example.com");
```
RedirectToActionResult: Represents a redirection to a specific action.
```
return RedirectToAction("ActionName", "ControllerName");
```
StatusCodeResult and ObjectResult: Represents a command that produces a status code response. ObjectResult can also produce an object in the body of the response.
object
```
return new StatusCodeResult(200); // Just the status code
return new ObjectResult(myObject) { StatusCode = 200 }; // Status code with object
```
NotFoundResult and NotFoundObjectResult: Represents a command that produces a 404 status code response. NotFoundObjectResult can also produce an object in the body of the response.
```
return NotFound(); // Just the status code
return NotFound(myObject); // Status code with object
```
OkResult and OkObjectResult: Represents a command that produces a 200 status code response. OkObjectResult can also produce an object in the body of the response.
```
return Ok(); // Just the status code
return Ok(myObject); // Status code with object
```
ActionResult\<T\>: Represents either an IActionResult or a specifc type `T`. The framework checks if the return object is an IActionResult and executes if it is. If it's not, it can return an object of type `T`.
```
public ActionResult<Project> GetProject() {
    Project project;
    // do stuff
    return project;
}
```
These are just a few examples. There are many other types that implement IActionResult to represent different types of HTTP responses.