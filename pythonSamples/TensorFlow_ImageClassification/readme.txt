
- source
	https://www.youtube.com/watch?v=cSKfRcEDGUs
	https://codelabs.developers.google.com/codelabs/tensorflow-for-poets/?utm_campaign=chrome_series_machinelearning_063016

- download data
	curl -O http://download.tensorflow.org/example_images/flower_photos.tgz


- download training code
	curl -O https://raw.githubusercontent.com/tensorflow/tensorflow/r1.1/tensorflow/examples/image_retraining/retrain.py

- training
	python retrain.py \
  		--bottleneck_dir=bottlenecks \
  		--how_many_training_steps=500 \
  		--model_dir=inception \
  		--summaries_dir=training_summaries/basic \
  		--output_graph=retrained_graph.pb \
  		--output_labels=retrained_labels.txt \
  		--image_dir=flower_photos

- board
	run 'tensorboard --logdir=training_summaries/basic'
	open http://localhost:6006/#graphs

- test
	download test program
	curl -L https://goo.gl/3lTKZs > label_image.py
	run 'python label_image.py flower_photos/daisy/21652746_cc379e0eea_m.jpg'


==== file 'label_image.py' content ===========================================================================================

import os, sys

import tensorflow as tf

os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'

# change this as you see fit
image_path = sys.argv[1]

# Read in the image_data
image_data = tf.gfile.FastGFile(image_path, 'rb').read()

# Loads label file, strips off carriage return
label_lines = [line.rstrip() for line 
                   in tf.gfile.GFile("retrained_labels.txt")]

# Unpersists graph from file
with tf.gfile.FastGFile("retrained_graph.pb", 'rb') as f:
    graph_def = tf.GraphDef()
    graph_def.ParseFromString(f.read())
    tf.import_graph_def(graph_def, name='')

with tf.Session() as sess:
    # Feed the image_data as input to the graph and get first prediction
    softmax_tensor = sess.graph.get_tensor_by_name('final_result:0')
    
    predictions = sess.run(softmax_tensor, \
             {'DecodeJpeg/contents:0': image_data})
    
    # Sort to show labels of first prediction in order of confidence
    top_k = predictions[0].argsort()[-len(predictions[0]):][::-1]
    
    for node_id in top_k:
        human_string = label_lines[node_id]
        score = predictions[0][node_id]
        print('%s (score = %.5f)' % (human_string, score))

==== file 'label_image.py' content ===========================================================================================