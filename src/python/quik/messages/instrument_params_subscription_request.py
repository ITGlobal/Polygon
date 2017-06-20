from polygon.quik.messages.message import QLMessage


class QLInstrumentParamsSubscriptionRequest(QLMessage):
    def __init__(self, instrument):
        QLMessage.__init__(self, "InstrumentParamsSubscriptionRequest")
        self.instrument = instrument