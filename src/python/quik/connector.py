import socket
import sys
import time
import threading
import queue
import logging
from .messages import *


class QuikConnector:
    def __init__(self):
        """
        Конструктор
        """
        self.callbacks = []
        self.s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.receivedQueue = queue.Queue()
        self.sendingQueue = queue.Queue()
        self.threadReceive = threading.Thread(target=self.__receive_thread__, name="receiving thread")
        self.threadSend = threading.Thread(target=self.__send_thread__, name="sending thread")
        self.threadDispatch = threading.Thread(target=self.__dispatch_thread__, name="dispatching thread")

    def connect(self, ip="127.0.0.1", port=1248):
        """Подключиться к квику

        :param str ip: IP адрес с формате строки
        :param int port: Порт, на котором слушает терминал
        """
        try:
            self.s.connect((ip, port))
            self.running = True
            self.threadSend.start()
            self.threadReceive.start()
            self.threadDispatch.start()
        except:
            self.__log_last_errors__("Error while connecting to QUIK")


    def disconnect(self):
        """Отключиться от терминала

        :rtype: None
        """
        self.running = False
        time.sleep(1)  # даём потокам завершиться
        self.f.close()
        self.s.close()

    def send(self, message):
        """Отправить сообщение в терминал

        :param QLMessage message: Сообщение, наследник класса QLMessage
        """
        self.sendingQueue.put(message)

    def subscribe(self, callback):
        """Подписаться на событие о приходе нового сообщения

        :param function callback: Функция обратного вызова
        """
        self.callbacks.append(callback)

    def __receive_thread__(self):
        """Поток получения сообщений от квика
        """
        try:
            self.f = self.s.makefile()
            while self.running:
                line = self.f.readline()
                while len(line) > 0:
                    env = QLEnvelope(line)
                    self.receivedQueue.put(env)
                    self.sendingQueue.put(QLEnvelopeAcknowledgment(env.id))
                    line = self.f.readline()
        except:
            self.__log_last_errors__("Error while receiving from QUIK")

    def __dispatch_thread__(self):
        """Поток распаковки сообщений и отправки их подписчикам
        """
        try:
            while self.running:
                if self.receivedQueue.empty():
                    time.sleep(0.2)

                env = self.receivedQueue.get()

                for message in env.body:
                    print(message)
        except:
            self.__log_last_errors__("Error while dispatching messages")

    def __send_thread__(self):
        """Поток отправки сообщений в квик
        """
        try:
            while self.running:
                if self.sendingQueue.empty():
                    time.sleep(0.2)

                message = self.sendingQueue.get()
                self.s.send(message.toJSON().encode())
        except:
            self.__log_last_errors__("Error while sending to QUIK")

    def __log_last_errors__(self, message):
        """Залоггировать последние ошибки sys.exc_info()

        :param message: Сообщение для вывода в лог перед ошибками
        """
        logging.error(message)
        err = sys.exc_info()
        for i in range(0, len(sys.exc_info())):
            logging.error("{0}".format(err[i]))

