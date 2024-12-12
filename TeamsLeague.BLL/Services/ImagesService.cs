using TeamsLeague.BLL.Interfaces;
using TeamsLeague.DAL.Constants.Match.GamePerson;
using TeamsLeague.DAL.Constants.Member;

namespace TeamsLeague.BLL.Services
{
    public class ImagesService : IImagesService
    {
        public static string GetPositionImageUrl(PositionType type)
        {
            return type switch
            {
                PositionType.Top => "/Resources/Img/Default/Roles/1.png",
                PositionType.Jungle => "/Resources/Img/Default/Roles/2.png",
                PositionType.Mid => "/Resources/Img/Default/Roles/3.png",
                PositionType.Bot => "/Resources/Img/Default/Roles/4.png",
                PositionType.Support => "/Resources/Img/Default/Roles/5.png",
                _ => throw new NotImplementedException("Unknown position type."),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgNum">Number should be from 38 to 99.</param>
        /// <returns></returns>
        public static string GetTeamImgUrl(int imgNum) => $"/Resources/Img/Default/Teams/{imgNum}.jpg";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgNum">Number should be from 38 to 99.</param>
        /// <returns></returns>
        public static string GetCharImgUrl(GameCharType type) => $"/Resources/Img/Default/Char/{type}.jpg";
    }
}
