window.gh = {
    confirmAction(message) {
        return window.confirm(message);
    },
    scrollToComposer() {
        const target = document.getElementById('composer');
        if (!target) {
            return;
        }

        target.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
};
