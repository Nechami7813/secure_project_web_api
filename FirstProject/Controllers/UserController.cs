using Entities;
using Microsoft.AspNetCore.Mvc;
using Service.user;
using Zxcvbn;

namespace FirstProject.Controllers;
[ApiController]

[Route("api/[controller]")]


public class UserController : ControllerBase
{
    private IUserService _userService;
    private ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        this._userService = userService;
        _logger = logger;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get([FromQuery] string email, [FromQuery] string password)
    {
        var foundUser = await _userService.getUser(email, password);
        if (foundUser == null)
            return NoContent();
        else
            Response.Cookies.Append("X-Access-Token", foundUser.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

        return Ok(foundUser);
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {

        var users = await _userService.GetUsers();
        if (users.Count() > 0)
            return Ok(users);
        return NotFound();
    }
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register([FromBody] User user)
    {
        //var res = Zxcvbn.Core.EvaluatePassword(user.Password);
        //if (res.Score >= 2) { 
        User registerUser = await _userService.Register(user);
        if (registerUser != null)
            return Ok(registerUser);
        return NotFound();
    }
    [HttpPost("password")]
    public async Task<ActionResult<User>> CheckPassword([FromBody] Object passsword)
    {
        var res = Zxcvbn.Core.EvaluatePassword(passsword.ToString());
        if (res.Score >= 2)
        {
                return Ok(res.Score);
        }
        return NotFound(res.Score);
    }
    [HttpPost("login")]
    public async Task<ActionResult<User>> Login([FromBody] User user)
    {
        User loginUser = await _userService.Login(user.Email, user.Password);
        _logger.LogInformation($"Login attempted with User Name,{user.Email} and password{user.Password} ");

        if (loginUser != null)
            return Accepted(loginUser);
        return NotFound();
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser(int id ,[FromBody]User user)
    {
        var res = Zxcvbn.Core.EvaluatePassword(user.Password);
        if (res.Score >= 2)
        {
            User newUser = new User { UserId =user.UserId, Email = user.Email, FirstName = user.FirstName, Password = user.Password, LastName = user.LastName };

            User UpdateUser =await _userService.UpdateUser(id,newUser);
            if (UpdateUser != null)
                return Accepted(UpdateUser);
        }
        return NotFound();
    }
}


