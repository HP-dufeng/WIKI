using FileCenter.MassTransit.Event.Event;
using WIKI.EntityFramework;

namespace WIKI.MassTransit.Consumer.UploadEventConsumer
{
    public abstract class BaseUploadEventHandler
    {
        private WIKIDbContext _db;

        public WIKIDbContext Db
        {
            get
            {
                if (_db == null)
                {
                    _db = new WIKIDbContext();                    
                }
                return _db;
            }
        }

        public virtual void Do(UploadAttachmentEvent events) { }
    }
}
