namespace Tokiota.Redis.Protocol
{
    using Net;
    using System.IO;

    internal class Operation<T> : IOperation<T>
    {
        private readonly RedisSocket socket;
        private readonly IOperationRequest request;
        private readonly IOperationResponse<T> response;

        public Operation(RedisSocket socket, IOperationRequest request, IOperationResponse<T> response)
        {
            this.socket = socket;
            this.request = request;
            this.response = response;
        }

        public T Execute(params byte[][] commands)
        {
            if (this.SendRequest(commands))
            {
                return this.GetResponse();
            }

            throw new IOException("Could not send the commands");
        }

        protected virtual bool SendRequest(params byte[][] commands)
        {
            return this.request.Send(this.socket, commands);
        }

        protected virtual T GetResponse()
        {
            return this.response.Receive(this.socket);
        }
    }
}
