from ..messages.message import QLMessage

class QLEnvelopeAcknowledgment(QLMessage):
    def __init__(self, id):
        QLMessage.__init__(self, "EnvAck")
        self.id = id

