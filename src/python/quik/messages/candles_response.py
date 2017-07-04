from ..messages.message import QLMessage
import uuid

class QLCandlesResponse(QLMessage):
    def __init__(self, qlMessage):

        QLMessage.__init__(self, "CandlesResponse")
        self.__dict__ = qlMessage