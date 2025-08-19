import unittest
from src.No.no import No

class test_no(unittest.TestCase):
    def test_no_creation(self):
        no = No(5)
        self.assertEqual(no.valor, 5)
        self.assertEqual(no.proximo, None)

    def test_no_str(self):
        no = No(10)
        self.assertEqual(str(no), "10")

    def test_no_next(self):
        no1 = No(1)
        no2 = No(2)
        no1.proximo = no2
        self.assertEqual(no1.proximo.valor, 2)

if __name__ == '__main__':
    unittest.main()
