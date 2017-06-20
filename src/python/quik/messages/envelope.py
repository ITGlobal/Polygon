import json


class QLEnvelope:
    def __init__(self, jsonString):
        self.__dict__ = json.loads(jsonString)