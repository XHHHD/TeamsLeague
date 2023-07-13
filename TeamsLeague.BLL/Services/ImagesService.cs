using TeamsLeague.BLL.Interfaces;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Services
{
    public class ImagesService : IImagesService
    {
        public static string GetPositionImageUrl(PositionType type)
        {
            return type switch
            {
                PositionType.Top => "/Resources/Img/Default/icons8-ос-free-bsd-100-black.png",
                PositionType.Jungle => "/Resources/Img/Default/icons8-ос-free-bsd-100-black.png",
                PositionType.Middle => "/Resources/Img/Default/icons8-ос-free-bsd-100-black.png",
                PositionType.Bot => "/Resources/Img/Default/icons8-ос-free-bsd-100-black.png",
                PositionType.Support => "/Resources/Img/Default/icons8-ос-free-bsd-100-black.png",
                _ => throw new NotImplementedException("Unknown position type."),
            };
        }
    }
}
