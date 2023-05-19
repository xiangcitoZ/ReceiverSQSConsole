using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using ReceiverSQSConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverSQSConsole.Services
{
    public class ServiceSQS
    {
        private IAmazonSQS amazonSQS;
        private string UrlQueue;
        public ServiceSQS(IAmazonSQS amazonSQS)
        {
            this.amazonSQS = amazonSQS;
            this.UrlQueue = "https://sqs.us-east-1.amazonaws.com/686197779335/queue-viernes-xzx";
        }

        public async Task<List<Mensaje>> ReciveMessagesAsync()
        {
            ReceiveMessageRequest request = new ReceiveMessageRequest
            {
                QueueUrl = UrlQueue
                , MaxNumberOfMessages = 5
                , WaitTimeSeconds = 5
            };
            ReceiveMessageResponse response = 
                await this.amazonSQS.ReceiveMessageAsync(request);
            if(response.HttpStatusCode == System.Net.HttpStatusCode.OK) 
            { 
                if(response.Messages.Count != 0)
                {

                    List<Message> messages = response.Messages;
                    List<Mensaje> output = new List<Mensaje>();
                    foreach(Message msj in messages)
                    {
                        string json = msj.Body;
                        Mensaje data = 
                            JsonConvert.DeserializeObject<Mensaje>(json);
                        data.ReceiptHandle = msj.ReceiptHandle;
                        output.Add(data);
                    }
                    return output;

                }else
                {
                    return null;
                }

            }else
            {
                return null;
            }
        }

        public async Task DeleteMessageAsync(string receiptHandle)
        {
            DeleteMessageRequest request = new DeleteMessageRequest
            {
                QueueUrl = this.UrlQueue,
                ReceiptHandle = receiptHandle
            };
            DeleteMessageResponse response =
                await this.amazonSQS.DeleteMessageAsync(request);
        }


    }
}
