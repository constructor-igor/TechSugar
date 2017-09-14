import os
import numpy as np

from matplotlib.backends.backend_pdf import PdfPages
import matplotlib.pyplot as plt
from scipy.misc import imread

def plotImage(image_file):
    im = imread(image_file).astype(np.float32) / 255
    plt.imshow(im)
    a = plt.gca()
    a.get_xaxis().set_visible(False) # We don't need axis ticks
    a.get_yaxis().set_visible(False)

if __name__ == '__main__':
    print('[main] started')

    pdf_sample_folder = r'pdf_sample_data'
    output_pdf_file_path = os.path.join(pdf_sample_folder, 'pdf_sample.pdf')
    image_file_path_1 = os.path.join(pdf_sample_folder, 'image1.png')
    image_file_path_2 = os.path.join(pdf_sample_folder, 'image2.png')

    pp = PdfPages(output_pdf_file_path)
    # plt.subplot(121)
    plotImage(image_file_path_1)
    pp.savefig(plt.gcf()) # This generates page 1
    # plt.subplot(122)
    plotImage(image_file_path_2)
    # pp.savefig(plt.gcf()) # This generates page 1
    pp.savefig(plt.gcf()) # This generates page 2
    pp.close()

    print('[main] completed')