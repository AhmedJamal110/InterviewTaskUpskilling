namespace InterviewTask.API.Shared
{
    public class CanceletionState
    {
        private  CancellationToken _cancellationToken;

        public void SetCancellationToken(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
        }

        public CancellationToken Token => _cancellationToken;

    }
}
