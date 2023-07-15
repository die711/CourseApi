using System.Net;
using AutoMapper;
using CourseApi.Dto;
using CourseApi.Dto.Specifications;
using CourseApi.Dto.Teacher;
using CourseApi.Entities;
using CourseApi.Repositories.Interfaces;
using CourseApi.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;

namespace CourseApi.Services.Implementations;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TeacherService> _logger;
    protected ApiResponse _response;

    public TeacherService(ITeacherRepository teacherRepository, IMapper mapper, ILogger<TeacherService> logger)
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
        _logger = logger;
        _response = new();
    }

    public async Task<ApiResponse> ListAsync()
    {
        try
        {
            var teachers = await _teacherRepository.GetAll(tracked: false);
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<List<TeacherDto>>(teachers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error in all teacher";
        }

        return _response;
    }

    public async Task<ApiResponse> GetAllPagination(Params parameters)
    {
        try
        {
            var teachers =  _teacherRepository.GetAllPagination(parameters);
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<List<TeacherDto>>(teachers);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error getting the courses";
        }

        return _response;
    }

    public async Task<ApiResponse> FindByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _response.ErrorMessage = "Id incorrecto";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;
                return _response;
            }

            var teacher = await _teacherRepository.GetById(id);

            if (teacher == null)
            {
                _response.ErrorMessage = "Teacher not found";
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccessful = false;
                return _response;
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<TeacherDto>(teacher);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error in obtain a teacher";
        }

        return _response;
    }

    public async Task<ApiResponse> CreateAsync(TeacherCreateDto model)
    {
        try
        {
            var entity = _mapper.Map<Teacher>(model);
            await _teacherRepository.Create(entity);
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = entity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error creating the teacher";
        }

        return _response;
    }

    public async Task<ApiResponse> UpdateAsync(TeacherUpdateDto model, int id)
    {
        try
        {
            if (id != model.Id)
            {
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = "Id incorrect";
                return _response;
            }

            var teacher = await _teacherRepository.GetById(id);

            if (teacher == null)
            {
                _response.ErrorMessage = "Teacher don't found";
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccessful = false;
                return _response;
            }

            teacher.Name = model.Name;
            teacher.LastName = model.LastName;
            teacher.Description = model.Description;
            teacher.UpdateDate = DateTime.Now;

            await _teacherRepository.Update();
            _response.StatusCode = HttpStatusCode.NoContent;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error updating the teacher";
        }

        return _response;
    }

    public async Task<ApiResponse> RemoveAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                _response.ErrorMessage = "Id incorrecto";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;
                return _response;
            }

            var teacher = await _teacherRepository.GetById(id);

            if (teacher == null)
            {
                _response.ErrorMessage = "Teacher is not found";
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccessful = false;
                return _response;
            }

            await _teacherRepository.Remove(teacher);
            _response.StatusCode = HttpStatusCode.NoContent;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccessful = false;
            _response.ErrorMessage = "Error removing teacher";
        }

        return _response;
    }

    public async Task<ApiResponse> UpdatePartialAsync(JsonPatchDocument<TeacherUpdateDto> patchDto, int id)
    {
        if (id <= 0)
        {
            _response.ErrorMessage = "Id invalid";
            _response.IsSuccessful = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            return _response;
        }

        var teacher = await _teacherRepository.GetById(id);
        
        if (teacher == null)
        {
            _response.ErrorMessage = "Teacher is not found";
            _response.IsSuccessful = false;
            _response.StatusCode = HttpStatusCode.NotFound;
            return _response;
        }

        var teacherDto = _mapper.Map<TeacherUpdateDto>(teacher);
        
         patchDto.ApplyTo(teacherDto);
        
        
        return await UpdateAsync(teacherDto,id);
    }
}