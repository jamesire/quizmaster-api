using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using QuizMaster.Storage.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMaster.Storage.ActiveQuizzesTable
{
    public class ActiveQuizzesTable : IActiveQuizzesTable
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private readonly string TableName = "ActiveQuizzes";

        public ActiveQuizzesTable(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task AddQuiz(string id, string username, Difficulties difficulty)
        {
            var difficultyAsString = ((int)difficulty).ToString();
            var item = new Dictionary<string, AttributeValue>
            {
                { "Id", new AttributeValue { S = id } },
                { "TimeStamp", new AttributeValue { S = DateTime.Now.ToString() } },
                { "Username", new AttributeValue { S = username } },
                { "Difficulty", new AttributeValue { N = difficultyAsString } }
            };

            var itemRequest = new PutItemRequest
            {
                TableName = this.TableName,
                Item = item
            };

            await _dynamoDbClient.PutItemAsync(itemRequest);
        }

        public async Task DeleteQuiz(string id)
        {
            var item = new Dictionary<string, AttributeValue>
            {
                { "Id", new AttributeValue { S = id } }
            };

            var deleteRequest = new DeleteItemRequest
            {
                TableName = this.TableName,
                Key = item
            };

            await _dynamoDbClient.DeleteItemAsync(deleteRequest);
        }

        public async Task JoinQuiz(string id, string username)
        {
            var item = new Dictionary<string, AttributeValue>
            {
                { ":v_Id", new AttributeValue { S = id } },
            };

            var request = new QueryRequest
            {
                TableName = this.TableName,
                KeyConditionExpression = "Id = :v_Id",
                ExpressionAttributeValues = item
            };

            var quiz = await _dynamoDbClient.QueryAsync(request);


            if (quiz != null)
            {
                var timeStamp = quiz.Items.First().First(f => f.Key == "TimeStamp").Value.S;
                var existingUsers = quiz.Items.First().First(f => f.Key == "Username").Value.S;

                if(existingUsers.Split(',').Length >= 4)
                {
                    Console.WriteLine("Return to user - this quiz is full");
                }
                if (existingUsers.Split(',').Any(u => u.Equals(username)))
                {
                    //Return telling user that this username already exists
                    Console.WriteLine("Return to user - username already exists in this game");
                };

                var joinQuizItem = new Dictionary<string, AttributeValue>
                {
                    { ":v_Username", new AttributeValue { S = existingUsers + "," + username } }
                };

                var key = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { S = id } },
                    { "TimeStamp", new AttributeValue { S = timeStamp } }
                };

                var itemRequest = new UpdateItemRequest
                {
                    TableName = this.TableName,
                    Key = key,
                    UpdateExpression = "set Username = :v_Username",
                    ExpressionAttributeValues = joinQuizItem
                };

                await _dynamoDbClient.UpdateItemAsync(itemRequest);
            }
        }
    }
}
