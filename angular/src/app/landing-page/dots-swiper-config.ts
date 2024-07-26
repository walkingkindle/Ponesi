export interface DotsSwiperConfig {
  loop: boolean;
  speed: number;
  autoplay: {
    delay: number;
  };
  slidesPerView: string | number;
  breakpoints: {
    [key: number]: {
      slidesPerView: number;
      spaceBetween: number;
    };
  };
}

