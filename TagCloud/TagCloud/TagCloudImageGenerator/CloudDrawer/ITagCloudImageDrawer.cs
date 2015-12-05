using System.Drawing;
using TagCloud.TagCloudImageGenerator.TagCloudGenerators;

namespace TagCloud.TagCloudImageGenerator.CloudDrawer
{
    public interface ITagCloudImageDrawer
    {
        Image DrawTagCloudImage(Statistic statistic, ITagCloudGenerator tagCloudGenerator);
    }
}
