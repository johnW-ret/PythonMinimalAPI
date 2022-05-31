import pandas as pd
import requests

def get_scraper(formatter):
    return HtmlDataScraper(formatter)

class HtmlDataScraper:
    def __init__(self, formatter):
        self.__formatter__ = formatter

    @staticmethod
    def get_data_from_link(url):
        r = requests.get(url)
        data = pd.read_html(r.text)
        return data

    def get_data_from_ticker(self, ticker):
        url = self.__formatter__(ticker)
        return HtmlDataScraper.get_data_from_link(url)