using Polygon.Connector.CQGContinuum.WebAPI;

namespace Polygon.Connector.CQGContinuum
{
    internal interface ITimeBarReportHandler
    {
        void Process(TimeBarReport report, out bool shouldRemoveHandler);
    }
}

