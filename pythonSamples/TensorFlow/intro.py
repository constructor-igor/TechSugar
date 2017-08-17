# 
# https://habrahabr.ru/post/305578/
# 
import tensorflow as tf

def intro():
    print('\nintro')
    foo = []
    bar = foo
    print(bar == foo)
    print(foo is bar)
    print('id: {0} and {1}'.format(id(foo), id(bar)))

    foo.append(bar)
    print('foo: ', foo)

    # 
    # 
    graph = tf.get_default_graph()
    print('graph:', graph)
    print('opertaions:', graph.get_operations())

    input_value = tf.constant(1.0)
    operations = graph.get_operations()
    print('graph', graph)
    print('opertaions:', operations)
    print('operation: ', operations[0].node_def)
    print('const in tf: ', input_value)

    sess = tf.Session()
    result = sess.run(input_value)
    print('result: ', result)

    weight = tf.Variable(0.8)
    print('operations:')
    for op in graph.get_operations():
        print(op.name)
    output_value = weight * input_value
    print('operations:')
    for op in graph.get_operations():
        print(op.name)
    last_operation = graph.get_operations()[-1]
    print('last_operation: ', last_operation.name)
    for op_input in last_operation.inputs:
        print(op_input)

    init = tf.initialize_all_variables()
    sess.run(init)
    result = sess.run(output_value)
    print('result: ', result)
    # 
    # 
    x = tf.constant(1.0, name='input')
    w = tf.Variable(0.8, name='weight')
    y = tf.multiply(w, x, name='output')
    summary_writer = tf.summary.FileWriter('log_simple_graph', sess.graph)
    # 
    # run 'tensorboard --logdir=log_simple_graph'
    # open http://localhost:6006/#graphs

def intro_to_training():
    print('\nintro_to_training')
    sess = tf.Session()
    x = tf.constant(1.0, name='input')
    w = tf.Variable(0.8, name='weight')
    y = tf.multiply(w, x, name='output')
    y_ = tf.constant(0.0)
    loss = (y - y_)**2
    optim = tf.train.GradientDescentOptimizer(learning_rate=0.025)
    grads_and_vars = optim.compute_gradients(loss)
    sess.run(tf.global_variables_initializer())
    print(grads_and_vars)
    print('size:', len(grads_and_vars))
    result = sess.run(grads_and_vars[0][0])
    print('result: ', result)

    sess.run(optim.apply_gradients(grads_and_vars))
    result = sess.run(w)
    print('result: ', result)

    train_step = tf.train.GradientDescentOptimizer(learning_rate=0.025).minimize(loss)

    summary_y = tf.summary.scalar('output', y)
    summary_writer = tf.summary.FileWriter('log_simple_stats')
    sess.run(tf.global_variables_initializer())
    for i in range(100):
        # print('before step {}, y is {}'.format(i, sess.run(y)))
        summary_str = sess.run(summary_y)
        summary_writer.add_summary(summary_str, i)
        sess.run(train_step)
    result = sess.run(y)
    print('result: ', result)

def final_intro_code():
    x = tf.constant(1.0, name='input')
    w = tf.Variable(0.8, name='weight')
    y = tf.multiply(w, x, name='output')
    y_ = tf.constant(0.0, name='correct_value')
    loss = tf.pow(y - y_, 2, name='loss')
    train_step = tf.train.GradientDescentOptimizer(0.025).minimize(loss)

    for value in [x, w, y, y_, loss]:
        tf.summary.scalar(value.op.name, value)

    summaries = tf.summary.merge_all()

    sess = tf.Session()
    summary_writer = tf.summary.FileWriter('log_simple_stats', sess.graph)

    sess.run(tf.global_variables_initializer())
    for i in range(100):
        summary_writer.add_summary(sess.run(summaries), i)
        sess.run(train_step)

if __name__ == '__main__':
    print('main started')
    print("tensorflow version: {0}".format(tf.__version__))
    # intro()
    # intro_to_training()
    final_intro_code()
    print('main completed')