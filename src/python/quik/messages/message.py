import json

class QLMessage:
    def __init__(self, message_type):
        self.message_type = message_type

    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__) + '\n'
