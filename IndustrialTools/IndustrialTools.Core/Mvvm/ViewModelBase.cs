using Prism.Mvvm;
using Prism.Navigation;

namespace IndustrialTools.Core.Mvvm
{
    public abstract class ViewModelBase : BindableBase, IDestructible
    {
        protected ViewModelBase()
        {

        }      

        public virtual void Destroy()
        {

        }
    }
}
