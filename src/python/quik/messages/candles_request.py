from ..messages.message import QLMessage
import uuid


class QLCandlesRequest(QLMessage):
    def __init__(self, instrument, span):
        """
        :param instrument: Instrument code
        :param span: Values: 1MIN, 5MIN, 10MIN, 15MIN, 30MIN, 1H, 4H, 1DAY, 1WEEK, 1MONTH
        """
        QLMessage.__init__(self, "CandlesRequest")
        self.instrument = instrument
        self.span = span
        self.id = str(uuid.uuid4())