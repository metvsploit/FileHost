using FileHost.WebApi.DataAccess;
using FileHost.WebApi.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FileHost.WebApi.Services
{
    public class FileService
    {
        private readonly IMongoCollection<FileData>? _fileCollection;

        public FileService(IOptions<FileHostDatabaseSettings> fileHostDbSettings)
        {
            var mongoClient = new MongoClient(fileHostDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(fileHostDbSettings.Value.DatabaseName);
            _fileCollection = mongoDatabase.GetCollection<FileData>(fileHostDbSettings.Value.CollectionName);
        }

        public async Task<Response<FileData>> GetAsync(string id)
        {
            var response = new Response<FileData>();

            try
            {
                var fileData = await _fileCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (fileData == null)
                {
                    response.Success = false;
                    response.Message = "Файл не найден";
                }
                else
                {
                    response.Success = true;
                    response.Message = "Файл найден";
                    response.Data = fileData;
                }
                return response;
            }
            catch (Exception ex)
            {
                return new Response<FileData> { Success = false, Message = ex.Message };
            }
        }

        public async Task<Response<FileData>> CreateAsync(FileData entity)
        {
            var response = new Response<FileData>();

            try
            {
                if (entity.Size > 104857600)
                {
                    response.Success = false;
                    response.Message = "Размер файла превышает 100мб";
                    return response;
                }
                await _fileCollection.InsertOneAsync(entity);
                response.Success = true;
                response.Message = "Файл успешно загружен";
                response.Data = entity;
                return response;
            }
            catch (Exception ex)
            {
                return new Response<FileData> { Message = ex.Message, Success = false, Data = entity };
            }
        }
    }
}
