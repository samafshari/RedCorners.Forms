using System.Threading.Tasks;

namespace RedCorners.Forms.Services
{
    public abstract class SettingsServiceBase<T> where T : class, new()
    {
        readonly Persistent<T> settings = new Persistent<T>();

        public T Data => settings.Data;

        public async Task SaveAsync()
        {
            await settings.SaveAsync();
        }

        public void SaveWithLock()
        {
            settings.SaveWithLock();
        }

        public void QueueSave()
        {
            settings.QueueSave();
        }
    }
}