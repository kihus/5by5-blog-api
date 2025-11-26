using Blog.Controllers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase, ITagController
{
	[HttpGet]
	[Route("HeartBeat")]
	public ActionResult<string> HeartBeat()
	{
		return Ok("Still alive!");
	}
}
