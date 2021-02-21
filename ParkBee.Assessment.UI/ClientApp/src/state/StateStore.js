const store = {};
const hub = {};

class _StateStore {
  subscribe = (topic, context) => {
    if (!hub[topic]) hub[topic] = [];

    hub[topic].push(context);
  };

  unsubscribe = (topic, context) => {
    if (!hub[topic]) return;

    hub[topic].map((c, i) => {
      if (c === context) hub[topic].splice(i, 1);
    });

    if (!hub[topic].length) delete hub[topic];
  };

  publish = (topic, message, updateStore) => {
    if (hub[topic])
      hub[topic].map(c => {
        let toSet = {};
        toSet[topic] = message;
        c.setState && c.setState(toSet);
      });

    if (updateStore) this.store(topic, message);
  };

  store = (topic, message) => {
    store[topic] = message;
  };

  retrieve = topic => store[topic] || null;
}

export const StateStore = new _StateStore();
