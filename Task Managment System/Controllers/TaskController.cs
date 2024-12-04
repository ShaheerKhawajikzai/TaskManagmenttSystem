using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Task_Managment_System.Models;
using Task_Managment_System.Repository.IRepository;

namespace Task_Managment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;
        private readonly APIResponse<Tasks> _apiResponse;
        public TaskController(ITaskRepository TaskRepository)
        {
            _apiResponse = new();
            _taskRepo = TaskRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse<List<Tasks>>>> GetTasks()
        {
            APIResponse<List<Tasks>> apiResponse = new();

            try
            {
                var listOfTasksFromDB = await _taskRepo.GetAllAsync();

                apiResponse.Result = listOfTasksFromDB;

                return Ok(apiResponse);
            }

            catch (Exception ex)
            {
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessages.Add(ex.ToString());
            }

            return StatusCode(Convert.ToInt32(apiResponse.StatusCode), apiResponse);
        }

        [HttpGet("{id:int}", Name = "GetTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse<Tasks>>> GetTask([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_apiResponse);
                }

                var task = await _taskRepo.GetAsync(x => x.TaskId == id);

                if (task == null)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessages.Add("Cannot find Task against id: " + id);

                    return NotFound(_apiResponse);
                }

                _apiResponse.Result = task;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add(ex.ToString());
            }

            return _apiResponse;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse<Tasks>>> CreateTask([FromBody] Tasks model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

                    return BadRequest(ModelState);
                }

                //Custom Validation
                // Check if the tasks with same name exists.
                if (await _taskRepo.GetAsync(x => x.Title.ToLower() == model.Title.ToLower()) != null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;
                    _apiResponse.ErrorMessages.Add("Task Already Exists. Please change your title");

                    return BadRequest(_apiResponse);
                }


                if (model == null)
                {
                    return BadRequest(model);
                }


                await _taskRepo.CreateAsync(model);

                _apiResponse.Result = model;
                _apiResponse.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetTask", new { id = model.TaskId }, _apiResponse);
            }

            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add(ex.Message.ToString());
            }

            return _apiResponse;
        }


        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse<Tasks>>> DeleteTask(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_apiResponse);
                }

                var task = await _taskRepo.GetAsync(x => x.TaskId == id);

                if (task == null)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessages.Add("Task does not exsit against id: " + id);

                    return NotFound(_apiResponse);
                }

                await _taskRepo.RemoveAsync(task);

                _apiResponse.StatusCode = HttpStatusCode.NoContent;

                return Ok(_apiResponse);

            }

            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add(ex.Message.ToString());
            }

            return _apiResponse;
        }


        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse<Tasks>>> UpdateTask([FromRoute] int id, Tasks model)
        {
            try
            {
                //TaskId must be greater than 0 and the TaskId from route and in the model should match.
                if (model.TaskId <= 0 || model.TaskId != id)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_apiResponse);
                }

                //Ef keeps track of model but here i have 2 models one coming
                //from the request object and one is below,
                //So ef cannot keep track of entites with the same TrackId,
                //if i dont use this isTracked  we will get an exception
                //unless we dont get the trackid in request objet.
                var task = await _taskRepo.GetAsync(x => x.TaskId == model.TaskId, isTracked: false);

                if (task == null)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessages.Add("Cannot find task");

                    return NotFound(_apiResponse);
                }



                await _taskRepo.UpdateAsync(model);

                _apiResponse.StatusCode = HttpStatusCode.NoContent;

                return Ok(_apiResponse);

            }

            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add(ex.Message.ToString());
            }

            return _apiResponse;
        }

    }
}
