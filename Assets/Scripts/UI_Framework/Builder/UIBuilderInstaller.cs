using Zenject;

namespace UI.Framework
{
    public class UIBuilderInstaller
    {
        private readonly DiContainer _parentContainer;
        
        public UIBuilderInstaller(DiContainer parentContainer)
        {
            _parentContainer = parentContainer;
        }
        
        public ViewCreator CreateContext()
        {
            return new ViewCreator(CreateContextInternal());
        }
        
        public ViewCreator CreateContext<TParam1,TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3)
        {
            return new ViewCreator(CreateContextInternal(param1, param2, param3));
        }
        
        public ViewCreator CreateContext<TParam1,TParam2>(TParam1 param1, TParam2 param2)
        {
            return new ViewCreator(CreateContextInternal(param1, param2));
        }
        
        public ViewCreator CreateContext<TParam>(TParam param)
        {
            return new ViewCreator(CreateContextInternal(param));
        }
        
        private DiContainer CreateContextInternal()
        {
            return _parentContainer;
        }
        
        private DiContainer CreateContextInternal<TParam1,TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3)
        {
            var container = CreateContextInternal(param1, param2);
            container.Bind<TParam3>()
                .FromInstance(param3)
                .AsSingle();
            
            return container;
        } 
        
        private DiContainer CreateContextInternal<TParam1,TParam2>(TParam1 param1, TParam2 param2)
        {
            var container = CreateContextInternal(param1);
            container.Bind<TParam2>()
                .FromInstance(param2)
                .AsSingle();
            
            return container;
        } 
        
        private DiContainer CreateContextInternal<TParam>(TParam param)
        {
            var subContainer = _parentContainer.CreateSubContainer();
            subContainer.Bind<TParam>()
                .FromInstance(param)
                .AsSingle();

            return subContainer;
        } 
        
    }
}