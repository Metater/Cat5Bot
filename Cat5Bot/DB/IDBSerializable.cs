namespace Cat5Bot.DB; //{}

public interface IDBSerializable
{
    public void Serialize(DBWriter dbWriter);
    public void Deserialize(DBReader dbReader);
}
