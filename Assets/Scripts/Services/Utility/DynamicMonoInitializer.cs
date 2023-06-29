using System;

namespace Services.Utility
{
    public class DynamicMonoInitializer<TParameter>
    {
        private readonly TParameter _parameter;

        public DynamicMonoInitializer(TParameter parameter)
        {
            _parameter = parameter;
        }
        
        public void Initialize(Action<TParameter> initAction)
        {
            initAction(_parameter);
        }
    }
    
    public class DynamicMonoInitializer<TParameter1, TParameter2>
    {
        private readonly TParameter1 _parameter1;
        private readonly TParameter2 _parameter2;
        
        public DynamicMonoInitializer(TParameter1 parameter1, TParameter2 parameter2) 
        {
            _parameter1 = parameter1;
            _parameter2 = parameter2;
        }
        
        public void Initialize(Action<TParameter1, TParameter2> initAction)
        {
            initAction(_parameter1, _parameter2);
        }
    }
    
    public class DynamicMonoInitializer<TParameter1, TParameter2, TParameter3>
    {
        private readonly TParameter1 _parameter1;
        private readonly TParameter2 _parameter2;
        private readonly TParameter3 _parameter3;
        
        public DynamicMonoInitializer(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3) 
        {
            _parameter1 = parameter1;
            _parameter2 = parameter2;
            _parameter3 = parameter3;
        }
        
        public void Initialize(Action<TParameter1, TParameter2, TParameter3> initAction)
        {
            initAction(_parameter1, _parameter2, _parameter3);
        }
    }
    
    public class DynamicMonoInitializer<TParameter1, TParameter2, TParameter3, TParameter4>
    {
        private readonly TParameter1 _parameter1;
        private readonly TParameter2 _parameter2;
        private readonly TParameter3 _parameter3;
        private readonly TParameter4 _parameter4;
        
        public DynamicMonoInitializer(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4) 
        {
            _parameter1 = parameter1;
            _parameter2 = parameter2;
            _parameter3 = parameter3;
            _parameter4 = parameter4;
        }
        
        public void Initialize(Action<TParameter1, TParameter2, TParameter3, TParameter4> initAction)
        {
            initAction(_parameter1, _parameter2, _parameter3, _parameter4);
        }
    }
    
    public class DynamicMonoInitializer<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>
    {
        private readonly TParameter1 _parameter1;
        private readonly TParameter2 _parameter2;
        private readonly TParameter3 _parameter3;
        private readonly TParameter4 _parameter4;
        private readonly TParameter5 _parameter5;
        
        public DynamicMonoInitializer(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4, TParameter5 parameter5) 
        {
            _parameter1 = parameter1;
            _parameter2 = parameter2;
            _parameter3 = parameter3;
            _parameter4 = parameter4;
            _parameter5 = parameter5;
        }
        
        public void Initialize(Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5> initAction)
        {
            initAction(_parameter1, _parameter2, _parameter3, _parameter4, _parameter5);
        }
    }
}