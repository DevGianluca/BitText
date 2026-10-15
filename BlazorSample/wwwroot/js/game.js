let scrollFrame;
let originalScrollBehavior;

export function scrollToGameControls() {
    const controls = document.querySelector(".game-controls");
    if (!controls) {
        return;
    }

    if (window.matchMedia("(prefers-reduced-motion: reduce)").matches) {
        controls.scrollIntoView({ behavior: "instant", block: "center" });
        return;
    }

    if (scrollFrame !== undefined) {
        cancelAnimationFrame(scrollFrame);
        document.documentElement.style.scrollBehavior = originalScrollBehavior;
    }

    const root = document.documentElement;
    originalScrollBehavior = root.style.scrollBehavior;
    root.style.scrollBehavior = "auto";

    const start = window.scrollY;
    const maxScroll = root.scrollHeight - window.innerHeight;
    const target = Math.max(
        0,
        Math.min(
            maxScroll,
            window.scrollY + controls.getBoundingClientRect().top
                - (window.innerHeight - controls.offsetHeight) / 2
        )
    );
    const startTime = performance.now();
    const duration = 750;

    const animate = (now) => {
        const progress = Math.min((now - startTime) / duration, 1);
        const easedProgress = progress < 0.5
            ? 4 * progress * progress * progress
            : 1 - Math.pow(-2 * progress + 2, 3) / 2;

        window.scrollTo(0, start + (target - start) * easedProgress);

        if (progress < 1) {
            scrollFrame = requestAnimationFrame(animate);
        } else {
            scrollFrame = undefined;
            root.style.scrollBehavior = originalScrollBehavior;
        }
    };

    scrollFrame = requestAnimationFrame(animate);
}
