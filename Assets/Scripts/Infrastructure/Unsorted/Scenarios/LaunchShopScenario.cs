using UI.Shop;
using UnityEngine;
using Zenject;

namespace Infrastructure.Scenraios
{
    public class LaunchShopScenario : MonoBehaviour
    {
        private ShopScreen.Factory _shopScreenFactory;

        [Inject]
        public void Construct(ShopScreen.Factory shopScreenFactory)
        {
            _shopScreenFactory = shopScreenFactory;
        }

        protected void Start()
        {
            _shopScreenFactory.Open();
        }
    }
}