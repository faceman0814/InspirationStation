using System.Runtime.Serialization;

namespace FaceMan.Utils.Exception;

[Serializable]
public class InspirationStationException:System.Exception
{
    /// <summary>
    /// Creates a new <see cref="T:InspirationStation.InspirationStationException" /> object.
    /// </summary>
    public InspirationStationException()
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:InspirationStation.InspirationStationException" /> object.
    /// </summary>
    public InspirationStationException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:InspirationStation.InspirationStationException" /> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public InspirationStationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:InspirationStation.InspirationStationException" /> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public InspirationStationException(string message, System.Exception innerException)
        : base(message, innerException)
    {
    }
}