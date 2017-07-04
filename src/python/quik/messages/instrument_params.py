from ..messages.message import QLMessage

class QLInstrumentParams(QLMessage):
    def __init__(self, qlMessage):
        self.__dict__ = qlMessage