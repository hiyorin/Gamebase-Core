using UniRx.Async;
using UnityEngine;
using Zenject;

namespace Gamebase.Application.Data.Internal
{
    internal sealed class DataFetchController : IDataFetchController
    {
        private readonly IUserIdRepository userIdRepository;
        
        private readonly IFetchRepository[] repositories;

        public DataFetchController(
            [InjectOptional] IUserIdRepository userIdRepository,
            [InjectOptional] IFetchRepository[] repositories)
        {
            this.userIdRepository = userIdRepository;
            this.repositories = repositories;
        }
        
        #region IDataFetchController implementaion

        async UniTask IDataFetchController.Fetch()
        {
            if (repositories == null)
            {
                Debug.unityLogger.LogWarning(GetType().Name, "Not found fetch repositories");
                return;
            }

            string userId = null;
            if (userIdRepository != null)
            {
                userId = userIdRepository.UserId;
            }

            Debug.unityLogger.Log(GetType().Name, "Start Fetch");
            foreach (var repository in repositories)
            {
                Debug.unityLogger.Log(GetType().Name, $"Fetching {repository.GetType()}");
                await repository.Fetch(userId);
            }
            Debug.unityLogger.Log(GetType().Name, "End Fetch");
        }
        
        #endregion
    }
}