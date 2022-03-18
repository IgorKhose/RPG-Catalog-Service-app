using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using static MongoDB.Bson.BsonType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers(options =>
// {
// 	options.SuppressAsyncSuffixInActionNames = false;
// });
//builder.Services.AddControllers();
builder.Services.AddMvc(options =>
{
   options.SuppressAsyncSuffixInActionNames = false; 
});
// everytime we're going to store any document with guid type is going to be stored as a string
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
