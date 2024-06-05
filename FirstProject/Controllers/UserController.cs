using AutoMapper;
using DTOs;
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
    private IMapper _mapper;

    public UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper)
    {
        this._userService = userService;
        _logger = logger;
        _mapper = mapper;
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


    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<User>>> Get()
    //{

    //    var users = await _userService.GetUsers();
    //    if (users.Count() > 0)
    //        return Ok(users);
    //    return NotFound();
    //}
    [HttpPost("register")]
    public async Task<ActionResult<ReturnUserDto>> Register([FromBody] UserDTO userdto)
    {
        //var res = Zxcvbn.Core.EvaluatePassword(user.Password);
        //if (res.Score >= 2) { 
        var user = _mapper.Map<UserDTO, User>(userdto);
        User registerUser = await _userService.Register(user);

        ReturnUserDto resUser = _mapper.Map<User, ReturnUserDto>(registerUser);
        if (registerUser != null)
            return Ok(resUser);
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

    //public async Task<ActionResult<ReturnUserDto>> Login([FromBody] LoginUserDTO user)
    //{
    //    User loginUser = await _userService.Login(user.Email, user.Password);
    //    _logger.LogInformation($"Login attempted with User Name,{user.Email} and password{user.Password} ");
    //    ReturnUserDto resUser = _mapper.Map<User, ReturnUserDto>(loginUser);

    //    if (loginUser != null)
    //        return Accepted(resUser);
    //    return NotFound();
    //}
    public async Task<ActionResult<ReturnUserDto>> Login([FromBody] LoginUserDTO user)
    {
        User loginUser = await _userService.Login(user.Email, user.Password);
        _logger.LogInformation($"Login attempted with User Name,{user.Email} and password{user.Password} ");
        ReturnUserDto resUser = _mapper.Map<User, ReturnUserDto>(loginUser);
        if (loginUser != null)
        {
            var foundUser = await _userService.getUser(user.Email, user.Password);
            if (foundUser == null)
                return NoContent();
            else
                Response.Cookies.Append("X-Access-Token", foundUser.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            return Ok(resUser);
        }
        return NotFound();
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<ReturnUserDto>> UpdateUser(int id ,[FromBody] UserDTO user)
    {
        var res = Zxcvbn.Core.EvaluatePassword(user.Password);
        if (res.Score >= 2)
        {
            User newUser = _mapper.Map<UserDTO, User>(user);

            User UpdatuUser = await _userService.UpdateUser(id, newUser);
            ReturnUserDto resUser = _mapper.Map<User, ReturnUserDto>(UpdatuUser);
            if (UpdateUser != null)
                return Accepted(resUser);
        }
        return NotFound();
    }
}


