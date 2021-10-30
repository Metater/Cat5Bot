using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat5Bot.DB;

public interface IDBSerializable
{
    public void Serialize(DBWriter dbWriter);
    public void Deserialize(DBReader dbReader);
}
